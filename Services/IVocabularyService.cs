using EnglishVoNo.Data;

namespace EnglishVoNo.Services
{
    public interface IVocabularyService
    {
        Task<List<Vocabulary>> GetAllAsync();
        Task<Vocabulary?> GetAsync(string id, string deckId);
        Task CreateAsync(Vocabulary vocabulary);
        Task<List<Vocabulary>> GetByDeckAsync(string deckId);
        Task UpsertAsync(Vocabulary vocabulary);

        Task DeleteAsync(string id, string deckId);
    }
}
