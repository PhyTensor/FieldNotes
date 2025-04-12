using System.Linq.Expressions;
using Application.Models;
using Core.Entities;

namespace Application.Projections;

public static class NoteProjections
{
    public static NoteResponse ToResponse(this Note note)
        => new NoteResponse(
            note.Id,
            note.Title,
            note.Content,
            note.CreatedAt);

    public static IQueryable<NoteResponse> ProjectToResponse(this IQueryable<Note> notes)
    {
        return notes.Select(n => new NoteResponse(
            n.Id,
            n.Title,
            n.Content,
            n.CreatedAt
        ));
    }
}

// public static class NoteProjections
// {
//     public static readonly Expression<Func<Note, NoteProjections>> Selector = note => new NoteResponse(
//             note.Id,
//             note.Title,
//             note.Content,
//             note.CreatedAt
//
//
//             );
//
//     public static IQueryable<NoteResponse> ProjectToResponse(this IQueryable<Note> query)
//             => query.Select(Selector);
//
//     public static NoteResponse ToResponse(this Note note)
//         => new(note.Id, note.Title, note.Content, note.CreatedAt);
// }



