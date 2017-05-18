import { AUTH_LOGIN, AUTH_LOGOUT, AUTH_CHECK } from 'admin-on-rest';

const LOGIN_FAILED = "Incorrect username or password.";


export default (type, params) => {

    if (type === AUTH_LOGIN) {
        localStorage.setItem('lel', "123");
        return checkLogin(params.username,params.password);
    }
    else if (type === AUTH_LOGOUT) {
        localStorage.removeItem('username');
        return Promise.resolve();
    }
    else if (type === AUTH_CHECK) {
        return checkLogin(params.username,params.password);
    }
    return Promise.reject(LOGIN_FAILED);
};

function checkLogin( username,password){
    if(localStorage.getItem(username)==password){
        return Promise.resolve();
    }
    return Promise.reject(LOGIN_FAILED);

}   