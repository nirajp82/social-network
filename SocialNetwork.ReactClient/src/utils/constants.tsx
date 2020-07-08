export const BASE_SERVICE_URL = process.env.REACT_APP_API_BASE_URL ?? '';

//Navigation Link
export const NAV_HOME = '/'; 
export const NAV_LOGIN = '/Login'; 
export const NAV_REGISTER = '/Register'; 
export const NAV_ACTIVITY_DETAIL = '/activities'; 
export const NAV_ACTIVITIES = '/activities'; 
export const NAV_CREATE_ACTIVITY = '/createactivity'; 
export const NAV_MANAGE_ACTIVITY = '/manageactivity'; 
export const NAV_USER_PROFILE = '/profile'; 
export const NAV_NOT_FOUND = '/notfound'; 

//Security
export const AUTH_TOKEN_NAME = 'SN_JWT_Token';

//Followers/Following
export const PREDICATE_FOLLOWERS = 'followers';
export const PREDICATE_FOLLOWINGS = 'followings';
export const TAB_INDEX_FOLLOWERS = 3;
export const TAB_INDEX_FOLLOWINGS = 4;
export const PREDICATE_PAST = 'past';
export const PREDICATE_HOSTING = 'hosting';


//
export const PREDICATE_ALL = 'all';
export const PREDICATE_IS_GOING = 'isGoing';
export const PREDICATE_IS_HOST = 'isHost';
export const PREDICATE_START_DATE = 'startDate';