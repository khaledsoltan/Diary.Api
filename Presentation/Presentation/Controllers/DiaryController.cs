using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.ActionFilters;
using Presentation.Controllers.Base;
using Shared.DTOS.DiaryDto;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{



    /// <summary>
    /// Diary Contains details of all  Diary users, their DiaryId, and names.
    /// These Apis do most of the work of holding diary data , retrieving and storing
    /// </summary>
    [Authorize]
    [Route("api/Diaries")]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryController : ControllerBase
    {

        public const string H_GetAllDiaries = nameof(H_GetAllDiaries);
        public const string H_GetDiaryById = nameof(H_GetDiaryById);
        public const string H_CreateDiaryForUser = nameof(H_CreateDiaryForUser);
        public const string H_DeleteDiary = nameof(H_DeleteDiary);
        public const string H_UpdateDiaryForUser = nameof(H_UpdateDiaryForUser);


        
        private readonly IServiceLocator _service;

        /// <summary>
        /// Diary Contains details of all  Diary users, their DiaryId, and names.
        /// These Apis do most of the work of holding diary data , retrieving and storing
        /// </summary>
        public DiaryController(IServiceLocator service) => _service = service;

        /// <summary>
        /// @desc Get an List of Diaries split into groups the length of `PageSize`.
        /// </summary>
        /// <param name="diaryRequestParameters">
        /// * PageNumber : For get  page number needed
        /// * PageSize : For get length of pages
        /// * OrderBy : For sorting entities by one field or more . 
        ///   example : orderBy=name,age desc. 
        ///   @note : (Please leave a space between fields and orderBy type)               
        /// * Fields : For get specific fields from entity
        /// * SearchDiaryName : For search by diaryName
        /// </param>
        /// <returns>
        /// Return 
        /// * List Of DiaryDtoS.
        /// * Return all links describe all behaviors related Diary
        /// * Return IsSuccess bool type for get response result true or false.
        /// </returns>
        /// 


        [HttpGet("GetDiaries", Name = H_GetAllDiaries)]
        public async Task<IActionResult> GetDiaries([FromQuery] DiaryRequestParameter diaryRequestParameters)
        {
            var pagedResult = await  _service.DiaryService.GetAllDiariesByUserId(diaryRequestParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diaries);
        }


        /// <summary>
        /// @desc CreateDiary an List of Diaries.
        /// </summary>
        /// <param name="diaryDtoForCreate">
        /// * DiaryName : For Create DiaryName
        /// </param>
        /// <returns>
        /// Return 
        /// * Return DiaryDto Created.
        /// * Return IsSuccess bool type for get response result true or false.
        /// * Return all links describe all behaviors related Diary
        /// </returns>
        /// 
        [HttpPost("CreateDiary" , Name = H_CreateDiaryForUser)]
        public async Task<IActionResult> CreateDiary(DiaryDtoForCreate diaryDtoForCreate)
        {
            var result =await  _service.DiaryService.CreateDiary(diaryDtoForCreate);

            return Ok(result);
        }
        /// <summary>
        /// @desc DeleteDiaryForUser for delete diaryentity by diaryId.
        /// </summary>
        /// <param name="id">
        /// * DiaryName : For Get Diaryentity
        /// </param>
        /// <returns>
        /// Return 
        /// * Return DiaryDto Deleted.
        /// </returns>
        /// 
        [HttpDelete("DeleteDiaryForUser" , Name = H_DeleteDiary)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<OkApiResponse<string>> DeleteDiaryForUser(Guid id)
        {
            var result = await _service.DiaryService.DeleteDiaryForUser(id, trackChanges: false);
            return new OkApiResponse<string>("Deleted Item Sucessufly !!");
        }

        /// <summary>
        /// @desc UpdateDiary for update diaryentity by diaryId
        /// </summary>
        /// <param name="id">
        /// * id : For Get Diaryentity
        /// </param>
        /// <param name="updateDiaryDto">
        /// * updateDiaryDto : object  for pass values updateing. 
        /// </param>
        /// <returns>
        /// * Return DiaryDto Created.
        /// * Return IsSuccess bool type for get response result true or false.
        /// * Return all links describe all behaviors related Diary
        /// 
        [HttpPut("UpdateDiary",Name = H_UpdateDiaryForUser)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDiary(Guid id, [FromBody] UpdateDiaryDto updateDiaryDto)
        {

          var result = await  _service.DiaryService.UpdateDiary(id, updateDiaryDto, true);

            return Ok(result);
        }
        /// <summary>
        /// Get Diary  By Id.
        /// </summary>
        /// <param name="id">
        /// * DiaryId : For get diaryentity 
        /// </param>
        /// Return DiaryDto Entity.
        /// * Return IsSuccess bool type for get response result true or false.
        /// * Return all links describe all behaviors related Diary
        [HttpGet("GetDiaryById", Name = H_GetDiaryById)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetDiaryById(Guid id)
        {
            var result = await _service.DiaryService.GetDiaryById(id, trackChanges: false);
            return Ok(result);
        }
    }
}
