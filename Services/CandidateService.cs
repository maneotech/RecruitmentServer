using MongoDB.Driver;
using RecruitmentServer.Models;
using RecruitmentServer.Services.Interfaces;


namespace RecruitmentServer.Services
{
    public class CandidateService: ICandidateService
    {
        private readonly IMongoCollection<Candidate> _candidates;
        private readonly IOfferService _offerService;
        private readonly IDocumentService _documentService;


        public CandidateService(IDatabaseSettings settings, IMongoClient mongoClient, IOfferService offerService, IDocumentService documentService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _candidates = database.GetCollection<Candidate>(settings.CandidateCollectionName);
            _offerService = offerService;
            _documentService = documentService;
        }


        public List<Candidate> GetAll()
        {
            var filter = Builders<Candidate>.Filter.Empty; // Empty filter to match all documents

            return _candidates.Find(filter).ToList();
        }

        public Candidate GetCandidateById(string id)
        {
            return _candidates.Find(c => c.Id == id).FirstOrDefault();
        }


        public Candidate Create(Candidate candidate)
        {
            _candidates.InsertOne(candidate);
            return candidate;
        }
        public bool Update(Candidate candidate)
        {
            ReplaceOneResult result = _candidates.ReplaceOne(c => c.Id == candidate.Id, candidate);
            if (result.IsAcknowledged)
            {
                if (result.MatchedCount > 0)
                {
                    return true;
                }
            }

            return false;
        }
        public void Connect(string candidateId, string offerId)
        {
            Offer offer = _offerService.GetById(offerId);
            if (offer == null)
            {
                return;
            }

            if (offer.Candidates.IndexOf(candidateId) < 0)
            {
                offer.Candidates.Add(candidateId);
            }

            _offerService.Update(offer);
        }
        public void Disconnect(string candidateId, string offerId)
        {
            Offer offer = _offerService.GetById(offerId);
            if (offer == null)
            {
                return;
            }

            if (offer.Candidates.IndexOf(candidateId) >= 0)
            {
                offer.Candidates.Remove(candidateId);
            }

            _offerService.Update(offer);
        }

        public async Task<List<Candidate>> UploadPDFFiles(ICollection<IFormFile> files)
        {
            // Iterate over each file
            List<Candidate> candidates = new List<Candidate>();

            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    continue;
                }

                //save PDF temporarily
                string filepath = await _documentService.SaveTmpPDF(file);


                //extraction for readable pdf
                string extractedText = _documentService.ExtractTextFromPdf(filepath);

                //if nothing has been extracted (because it is not a readable pdf but an image pdf)
                if (extractedText.Length < Constants.MinimumSizeToAcceptAsAReadablePDF)
                {
                    extractedText = _documentService.ExtractTextFromNonReadablePdf(filepath);
                }

                //extract picture image
                string pictureUrl = _documentService.ExtractCandidateFace(filepath);

                //create the user from the extractedText
                string firstname = CVFieldExtractor.GetFirstname(extractedText);
                string lastname = CVFieldExtractor.GetLastname(extractedText); ;
                string currentLocation = CVFieldExtractor.GetCurrentLocation(extractedText);
                string field = CVFieldExtractor.GetField(extractedText); ;
                string jobTitle = CVFieldExtractor.GetJobTitle(field, extractedText);
                List<string> keywords = CVFieldExtractor.GetKeywords(field, extractedText);
                List<string> languages = CVFieldExtractor.GetLanguages(extractedText);
                string phoneNumber = CVFieldExtractor.GetPhoneNumber(extractedText);
                string email = CVFieldExtractor.GetEmail(extractedText);

                Candidate candidate = new Candidate()
                {
                    Firstname = firstname,
                    Lastname = lastname,
                    CurrentLocation = currentLocation,
                    Field = field,
                    JobTitle = jobTitle,
                    Keywords = keywords,
                    Languages = languages,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    CvUrl = filepath,
                    PictureUrl = pictureUrl
                };

                candidates.Add(candidate);
            }
            return candidates;
        }
      
        public List<Candidate> AcceptCandidates(List<Candidate> candidates)
        {
             _candidates.InsertMany(candidates);
            var insertedCandidates = _candidates.Find(candidate => candidates.Contains(candidate)).ToList();
            return insertedCandidates;
        }

        public List<DuplicateCandidate> GetDuplicatesCandidates(List<Candidate> candidates)
        {

            // Extract unique emails and phones from the list of candidates
            var emails = candidates.Select(c => c.Email).Distinct().ToList();
            var phones = candidates.Select(c => c.PhoneNumber).Distinct().ToList();

            // Build the query to check for duplicates
            var filter = Builders<Candidate>.Filter.Or(
                Builders<Candidate>.Filter.In(c => c.Email, emails),
                Builders<Candidate>.Filter.In(c => c.PhoneNumber, phones)
            );

            // Execute the query
            List<Candidate> duplicateCandidates = _candidates.Find(filter).ToList();

            List<DuplicateCandidate> duplicates = new();
            foreach ( var candidate in candidates )
            {
                Candidate? duplicatedCandidate = duplicateCandidates.FirstOrDefault(c => c.Email == candidate.Email && c.Id != candidate.Id);
                if ( duplicatedCandidate == null ) 
                {
                    duplicatedCandidate = duplicateCandidates.FirstOrDefault(c => c.PhoneNumber == candidate.PhoneNumber && c.Id != candidate.Id);
                }

                if (duplicatedCandidate == null)
                {
                    continue;
                }

                DuplicateCandidate duplicate = new DuplicateCandidate()
                {
                    Candidate = candidate,
                    Duplicate = duplicatedCandidate
                };

                duplicates.Add(duplicate);
            }

            return duplicates;
        }


        public Candidate IgnoreOrNotCandidate(string candidateId, bool ignore)
        {
            var filter = Builders<Candidate>.Filter.Eq("Id", candidateId);
            var update = Builders<Candidate>.Update.Set("Ignored", ignore);
            var options = new FindOneAndUpdateOptions<Candidate>
            {
                ReturnDocument = ReturnDocument.After
            };

            return _candidates.FindOneAndUpdate(filter, update, options);
        }
    }
}
