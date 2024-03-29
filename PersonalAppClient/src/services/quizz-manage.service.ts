import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { QuizzManage } from 'src/shared/model/quizz-manage';
import { ResponseData, ResponseDatas } from 'src/shared/model/response-data';

@Injectable({
  providedIn: 'root'
})
export class QuizzManageService {

  constructor(private httpClient: HttpClient) { }

  getQuizzs(pageIndex: number, pageSize: number) : Observable<ResponseDatas>{
    return this.httpClient.get(
      environment.baseUri + `quizz-manage/get-all-quizz/${pageIndex}/${pageSize}`);
  }

  getQuizzById(id: number) : Observable<ResponseData>{
    return this.httpClient.get(
      environment.baseUri + `quizz-manage/get-quizz-by-id/${id}`);
  }

  createQuizz(quizzToCreate : FormData) : Observable<ResponseData> {
    return this.httpClient.post(
      environment.baseUri + 'quizz-manage/create-quizz' , quizzToCreate);
  }

  updateQuizz(quizzToUpdate : QuizzManage) : Observable<ResponseData> {
    return this.httpClient.put(
      environment.baseUri + 'quizz-manage/update-quizz',quizzToUpdate);
  }

  removeQuizz(id: number) : Observable<ResponseData> {
    return this.httpClient.delete(
      environment.baseUri + `quizz-manage/delete-quizz/${id}`);
  }


}
