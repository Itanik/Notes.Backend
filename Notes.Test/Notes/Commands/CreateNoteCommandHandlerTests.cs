using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Test.Common;
using Xunit;

namespace Notes.Test.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            // Arrage
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "NoteName";
            var noteDetails = "NoteDetails";

            // Act
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetails,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId &&
                note.Title == noteName &&
                note.Details == noteDetails));
        }
    }
}
