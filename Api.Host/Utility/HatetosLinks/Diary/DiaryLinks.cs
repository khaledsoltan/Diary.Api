using Sciensoft.Hateoas.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.DTOS.DiaryDto;

namespace Api.Diary.Utility.HatetosLinks.Diary
{
    
    public static class DiaryLinks
    {
        public static void AddDiaryLinks(this IServiceCollection services) =>
          services
            .AddMvc()
            .AddLink(policy =>
            {
                policy
                  .AddPolicy<DiaryDto>(model =>
                  {
                      model
                      .AddSelf(m => m.Id, "This is a GET self link.")
                      .AddRoute(m => m.Id, BookController.UpdateBookById)
                      .AddRoute(m => m.Id, BookController.DeleteBookById)
                      .AddCustomPath(m => m.Id, "Edit", method: HttpMethods.Post, message: "Edits resource")
                      .AddCustomPath(m => $"/change/resource/state/?id={m.Id}", "ChangeResourceState", method: HttpMethods.Post, message: "Any operation in your resource.")
                      .AddExternalUri(m => m.Id, "https://my-domain.com/api/books/", "Custom Domain External Link")
                      .AddExternalUri(m => $"/search?q={m.Title}", "https://google.com", "Google Search External Links", message: "This will do a search on Google engine.");
                  });
            });
    }
}
