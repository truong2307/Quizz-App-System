import { Action } from "@ngrx/store";
import { NotificationModel } from "src/shared/model/notification";

export const GET_NOTIFICATIONS = 'GET_NOTIFICATIONS';
export const GET_NOTIFICATIONS_SUCCESS = 'GET_NOTIFICATIONS_SUCCESS';
export const SEEN_NOTIFICATION = 'SEEN_NOTIFICATION';
export const REMOVE_NOTIFICATION = 'REMOVE_NOTIFICATION';
export const NOTIFICATION_NEWEST = 'NOTIFICATION_NEWEST';
export const FETCH_NOTIFICATION_ERROR = 'FETCH_NOTIFICATION_ERROR';

export class GetNotificationsAction implements Action {
  readonly type = GET_NOTIFICATIONS;
}

export class GetNotificationsSuccessAction implements Action {
  readonly type = GET_NOTIFICATIONS_SUCCESS;
  constructor(public items: NotificationModel[]){}
}

export class NotificationsNewestAction implements Action {
  readonly type = NOTIFICATION_NEWEST;
  constructor(public item: NotificationModel){}
}

export class RemoveNotificationAction implements Action {
  readonly type = REMOVE_NOTIFICATION;
  constructor(public item: number){}
}

export class SeenNotificationAction implements Action {
  readonly type = SEEN_NOTIFICATION;
  constructor(public item: NotificationModel){}
}

export class FetchNotificationError implements Action {
  readonly type = FETCH_NOTIFICATION_ERROR;
  constructor(public error: string){}
}

export type NotificationAction =
| GetNotificationsAction
| SeenNotificationAction
| NotificationsNewestAction
| FetchNotificationError
| RemoveNotificationAction
| GetNotificationsSuccessAction
