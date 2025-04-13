using Application.Models;
using Application.Projections;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class NoteService : INoteService
{
    private readonly DataContext _context;
    private readonly ILogger<NoteService> _logger;

    public NoteService(DataContext context, ILogger<NoteService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteResponse> CreateAsync(CreateNoteRequest request)
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Note created: {@Note}", note);

        return note.ToResponse();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            _logger.LogWarning("Note with ID {Id} not found", id);
            return false;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<NoteResponse>> GetAllAsync()
    {
        return await _context.Notes.AsNoTracking().ProjectToResponse().ToListAsync();
    }

    public async Task<NoteResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Notes
        .Where(n => n.Id == id)
        .ProjectToResponse()
        .FirstOrDefaultAsync();
    }

    public async Task<NoteResponse?> UpdateAsync(Guid id, CreateNoteRequest request)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) return null;

        note.Title = request.Title;
        note.Content = request.Content;
        // note.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return note.ToResponse();
    }
}
