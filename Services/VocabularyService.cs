using EnglishVoNo.Data;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace EnglishVoNo.Services
{
    public class VocabularyService : IVocabularyService
    {
        private readonly Container _container;

        public VocabularyService(CosmosClient client)
        {
            _container = client.GetContainer("english", "vocabulary");
        }

        public async Task<List<Vocabulary>> GetAllAsync()
        {
            var results = new List<Vocabulary>();
            var iterator = _container.GetItemQueryIterator<Vocabulary>();
            while (iterator.HasMoreResults)
            {
                var page = await iterator.ReadNextAsync();
                results.AddRange(page);
            }
            return results;
        }

        public async Task CreateAsync(Vocabulary vocabulary) => await _container.CreateItemAsync(vocabulary, new PartitionKey(vocabulary.DeckId));


        public async Task<Vocabulary?> GetAsync(string id, string deckId)
        {
            try
            {
                var response = await _container.ReadItemAsync<Vocabulary>(id, new PartitionKey(deckId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<Vocabulary>> GetByDeckAsync(string deckId)
        {
            var query = new QueryDefinition(
                "SELECT * FROM c WHERE c.deckId = @deckId")
                .WithParameter("@deckId", deckId);

            var results = new List<Vocabulary>();
            var iterator = _container.GetItemQueryIterator<Vocabulary>(query);

            while (iterator.HasMoreResults)
            {
                var page = await iterator.ReadNextAsync();
                results.AddRange(page);
            }
            return results;
        }

        public async Task UpsertAsync(Vocabulary vocabulary) => await _container.UpsertItemAsync(vocabulary, new PartitionKey(vocabulary.DeckId));

        // DELETE
        public async Task DeleteAsync(string id, string deckId) => await _container.DeleteItemAsync<Vocabulary>(id, new PartitionKey(deckId));
    }
}
