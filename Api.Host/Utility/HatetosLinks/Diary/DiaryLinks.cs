using Sciensoft.Hateoas.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.DTOS.DiaryDto;
using Presentation.Diary.Controllers;

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
                      .AddRoute(m => m.Id, DiaryController.CreateDiaryForUser)
                      .AddRoute(m => m.Id, DiaryController.UpdateDiaryForUser)
                      .AddRoute(m => m.Id, DiaryController.DeleteDiary)
                      .AddRoute(m => m.Id, DiaryController.GetAllDiaries);
                  });
            });
    }
}
