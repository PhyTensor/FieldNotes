using Application.Models;

namespace Application.Services;

public interface INoteService
{
    Task<IEnumerable<NoteResponse>> GetAllAsync();
    Task<NoteResponse> GetByIdAsync(Guid id);
    Task<NoteResponse> CreateAsync(CreateNoteRequest request);
    Task<NoteResponse> UpdateAsync(Guid id, CreateNoteRequest request);
    Task<bool> DeleteAsync(Guid id);
}
