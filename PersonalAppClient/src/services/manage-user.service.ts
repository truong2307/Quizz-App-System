import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseDatas } from 'src/shared/model/ResponseData.interface';
import { UpdateUser } from 'src/shared/model/User.interface';

@Injectable({
  providedIn: 'root'
})
export class ManageUserService {

  constructor(private httpClient: HttpClient) { }

  getUsers() : Observable<ResponseDatas>{
    return this.httpClient.get(environment.baseUri + 'manage-user/get-users');
  }

  updateUser(data: UpdateUser) : Observable<ResponseDatas>{
    return this.httpClient.put(environment.baseUri + 'manage-user/update-user', data);
  }
}
