import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminSideRoutingModule } from './admin-side-routing.module';
import { HomeComponent } from './home/home.component';
import { CalendarComponent } from './calendar/calendar.component';
import { GuardAdminSide } from 'src/guard/guard.admin';
import { AddEventComponent } from './calendar/add-event/add-event.component';

//Material module
import { MaterialExampleModule } from 'src/shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { TranslateModule } from '@ngx-translate/core';
import { ManageUserComponent } from './manage-user/manage-user.component';
import { EditUserComponent } from './manage-user/edit-user/edit-user.component';
import { SharedModule } from 'src/shared/shared.module';
import { QuizzTopicComponent } from './quizz-topic/quizz-topic.component';
import { QuizzManageComponent } from './quizz-manage/quizz-manage.component';
import { QuizzTopicDetailComponent } from './quizz-topic/quizz-topic-detail/quizz-topic-detail.component';
import { QuizzEditComponent } from './quizz-manage/quizz-edit/quizz-edit.component';

@NgModule({
  declarations: [
    HomeComponent,
    CalendarComponent,
    AddEventComponent,
    ManageUserComponent,
    EditUserComponent,
    QuizzTopicComponent,
    QuizzManageComponent,
    QuizzTopicDetailComponent,
    QuizzEditComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminSideRoutingModule,
    FormsModule,
    SharedModule,
    MaterialExampleModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    TranslateModule,
  ],
  providers: [GuardAdminSide],
})
export class AdminSideModule { }
