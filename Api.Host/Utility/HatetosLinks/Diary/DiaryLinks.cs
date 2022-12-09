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
                          //.AddRoute(m => m.Id, DiaryController.H_GetDiaryById, message: "@Desc (Get Diary  By Id.) @Param (Id DiaryId For get diaryentity.) @return (return diaryentity and links description  all behaviors related diary.)")
                          .AddCustomPath(m => "/api/Diary/GetDiaries", DiaryController.H_GetAllDiaries, message: "@desc (Get an List of Diaries split into groups the length of `PageSize`.) @Param (diaryRequestParameters for get some specific values  * PageNumber : For get  page number needed. * PageSize : For get length of pages. * OrderBy : For sorting entities by one field or more . example : orderBy=name,age desc. * Fields : For get specific fields from entity. * SearchDiaryName : For search by diaryName. ) @return (return diaryentity created and links description  all behaviors related diary.)")
                          .AddRoute(m => m.Id, DiaryController.H_UpdateDiaryForUser, message: "@desc (UpdateDiary for update diaryentity by diaryId.) @param  (id : For Get Diaryentity , updateDiaryDto  updateDiaryDto : object  for pass values updateing.)")
                          .AddCustomPath(m => $"/api/Diary/CreateDiary", "Create_Diary", method: HttpMethods.Post, message: "@desc (CreateDiary for Create diaryentity .) @param (diaryDtoForCreate  diaryDtoForCreate : object  for Create diaryEntity.)  @return (return diaryentity created and links description  all behaviors related diary.)");

                  });
            });
    }
}
