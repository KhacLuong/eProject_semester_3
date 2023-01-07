var url = 'https://localhost:7000/api/Auth/login';

const form = document.getElementById('form-validation');
let key = 'accessToken';


form.addEventListener('submit', async function (e) {
    e.preventDefault();
    const email = document.getElementById('userName').value;
    const password = document.getElementById('password').value;

    try {
        const res = await axios.post(url, {
            email: email,
            password: password
        })
            .then(data => {
                if (data.status === 200) {
                    window.localStorage.setItem(key, JSON.stringify(data.data.data.accessToken));

                    window.localStorage.setItem('isAuthen', JSON.stringify(true));

                    window.location.href = "../../pages/index.html";
                }
            })
        console.log(res)
    } catch (err) {
        console.log(err)
    }
});

