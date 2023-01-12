function Check() {
    let isAuthen = window.localStorage.getItem('isAuthen');

    if (isAuthen != 'true') {
        window.location.href = "../pages/authen/sign-in.html";
    }
}

Check();