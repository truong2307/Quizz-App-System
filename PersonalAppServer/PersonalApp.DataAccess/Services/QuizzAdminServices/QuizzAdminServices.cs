﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalApp.DataAccess.Comparer;
using PersonalApp.DataAccess.Data.Repository.IRepository;
using PersonalApp.DataAccess.Services.ClaimUserServices;
using PersonalApp.Models.Dto;
using PersonalApp.Models.Entities;

namespace PersonalApp.DataAccess.Services.QuizzAdminServices
{
    public class QuizzAdminServices : IQuizzAdminServices
    {
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimUserServices _claimUserServices;

        public QuizzAdminServices(IMapper mapper
            , IUnitOfWork unitOfWork
            , IClaimUserServices claimUserServices
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimUserServices = claimUserServices;
            _responseDto = new ResponseDto();
        }

        public async Task<ResponseDto> CreateQuizz(QuizzCreateDto model)
        {

            var currUserId = _claimUserServices.GetCurrentUserId();
            var dataMap = _mapper.Map<QuizzTest>(model);

            if (dataMap.MultiplechoiceQuestions.Count > 0)
            {
                dataMap.MultiplechoiceQuestions.ToList().ForEach(c =>
                {
                    c.CreatedAt = DateTime.Now;
                    c.CreatedBy = currUserId;
                });
            }

            if (dataMap.EssayQuestions.Count > 0)
            {
                dataMap.EssayQuestions.ToList().ForEach(c =>
                {
                    c.CreatedAt = DateTime.Now;
                    c.CreatedBy = currUserId;
                });
            }

            dataMap.CreatedAt = DateTime.Now;
            dataMap.CreatedBy = currUserId;

            await _unitOfWork.QuizzTest.AddAsync(dataMap);
            await _unitOfWork.SaveChangeAsync();

            _responseDto.Result = _mapper.Map<QuizzDto>(dataMap);
            _responseDto.IsSuccess = true;

            return _responseDto;
        }

        public async Task<ResponseDto> DeleteQuizz(int quizzId)
        {

            var quizzData = await _unitOfWork.QuizzTest.GetAsync(c => c.Id == quizzId);
            if (quizzData == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = "Quizz not exist in system";
                return _responseDto;
            }

            await _unitOfWork.QuizzTest.DeleteAsync(quizzId);
            await _unitOfWork.SaveChangeAsync();
            _responseDto.IsSuccess = true;

            return _responseDto;
        }

        public async Task<ResponseDatas<QuizzDto>> GetQuizzs(int pageIndex, int pageSize)
        {
            var quizzList = await _unitOfWork.QuizzTest.GetAllAsync(queryEntity: c => c.Skip(pageSize * pageIndex).Take(pageSize)
               , include: c => c.Include(i => i.QuizzTopic)
               .Include(i => i.MultiplechoiceQuestions)
               .Include(i => i.EssayQuestions));

            var responseData = new ResponseDatas<QuizzDto>()
            {
                Datas = _mapper.Map<List<QuizzDto>>(quizzList),
                TotalItem = (await _unitOfWork.QuizzTest.GetAllAsync()).Count,
                IsSuccess = true,
            };

            return responseData;
        }

        public async Task<ResponseDto> UpdateQuizz(QuizzDto model)
        {
            var quizzInDb = await _unitOfWork.QuizzTest.GetAsync(c => c.Id == model.Id
            , include: c => c.Include(i => i.MultiplechoiceQuestions)
            .Include(i => i.EssayQuestions).Include(c => c.QuizzTopic)
            );

            if (quizzInDb == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = "Quizz not exist in system";
                return _responseDto;
            }

            var currUserId = _claimUserServices.GetCurrentUserId();
            var quizzMapFromRequest = _mapper.Map<QuizzTest>(model);

            quizzInDb.Title = quizzMapFromRequest.Title;
            quizzInDb.ExamTime = quizzMapFromRequest.ExamTime;
            quizzInDb.Level = quizzMapFromRequest.Level;
            quizzInDb.TopicId = quizzMapFromRequest.TopicId;
            quizzInDb.IsPublic = quizzMapFromRequest.IsPublic;
            quizzInDb.UpdatedAt = DateTime.Now;
            quizzInDb.UpdatedBy = currUserId;

            var multiplechoiceQuestionDelete = quizzInDb.MultiplechoiceQuestions
                .Except(quizzMapFromRequest.MultiplechoiceQuestions, new QuizzMultiplechoiceQuestionComparer()).ToList();
            var multiplechoiceQuestionAdd = quizzMapFromRequest.MultiplechoiceQuestions
                .Except(quizzInDb.MultiplechoiceQuestions, new QuizzMultiplechoiceQuestionComparer()).ToList();
            var multiplechoiceQuestionUpdate = quizzMapFromRequest.MultiplechoiceQuestions
                .Except(multiplechoiceQuestionDelete, new QuizzMultiplechoiceQuestionComparer()).Where(c => c.Id > 0).ToList();

            var essayQuestionsDelete = quizzInDb.EssayQuestions
                .Except(quizzMapFromRequest.EssayQuestions, new QuizzEssayQuestionComparer()).ToList();
            var essayQuestionsAdd = quizzMapFromRequest.EssayQuestions
                .Except(quizzInDb.EssayQuestions, new QuizzEssayQuestionComparer()).ToList();
            var essayQuestionsUpdate = quizzMapFromRequest.EssayQuestions
                .Except(essayQuestionsDelete, new QuizzEssayQuestionComparer()).Where(c => c.Id > 0).ToList();

            await _unitOfWork.QuizzMultiplechoiceQuestion.DeleteRangeAsync(multiplechoiceQuestionDelete);
            await _unitOfWork.QuizzEssayQuestion.DeleteRangeAsync(essayQuestionsDelete);

            quizzInDb.EssayQuestions.Clear();
            quizzInDb.MultiplechoiceQuestions.Clear();
            quizzInDb.MultiplechoiceQuestions = multiplechoiceQuestionUpdate;
            quizzInDb.EssayQuestions = essayQuestionsUpdate;

            foreach (var item in quizzInDb.MultiplechoiceQuestions)
            {
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = currUserId;
            }

            foreach (var item in quizzInDb.EssayQuestions)
            {
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = currUserId;
            }

            essayQuestionsAdd.ForEach(item =>
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedBy = currUserId;
                quizzInDb.EssayQuestions.Add(item);
            });

            multiplechoiceQuestionAdd.ForEach(item =>
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedBy = currUserId;
                quizzInDb.MultiplechoiceQuestions.Add(item);
            });

            await _unitOfWork.QuizzTest.UpdateAsync(quizzInDb);
            await _unitOfWork.SaveChangeAsync();

            _responseDto.Result = _mapper.Map<QuizzDto>(quizzInDb);
            _responseDto.IsSuccess = true;

            return _responseDto;
        }
    }
}
