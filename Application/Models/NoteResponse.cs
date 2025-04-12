namespace Application.Models;

public record NoteResponse(Guid Id, string Title, string Content, DateTime CreatedAt);
