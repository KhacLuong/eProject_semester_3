var url = 'https://localhost:7000/api/AdminUser/register';

const form = document.getElementById('form-validation');

form.addEventListener('submit', async function (e) {
    e.preventDefault();

    const image = document.getElementById('productImg').src;
    const name = document.getElementById('userName').value;
    const email = document.getElementById('userEmail').value;
    const phone = document.getElementById('phoneNumber').value;
    const dateofBirth = document.getElementById('dob').value;
    const gender = document.getElementById('userGender').value;
    const password = document.getElementById('userPassword').value;
    const addressLine1 = document.getElementById('userAddress1').value;
    const addressLine2 = document.getElementById('userAddress2').value;
    const country = document.getElementById('userCountry').value;
    const city = document.getElementById('userCity').value;
    const district = document.getElementById('userDistrict').value;
    const postcode = document.getElementById('userPostCode').value;
    const userType = document.getElementById('userType').value;

    try {
        const res = await axios.post(url, {
            avatar: image,
            name: name,
            email: email,
            phone: phone,
            dateofBirth: dateofBirth,
            gender: gender,
            password: password,
            addressLine1: addressLine1,
            addressLine2: addressLine2,
            country: country,
            city: city,
            district: district,
            postcode: postcode,
            userType: userType,
        })
            .then(data => {
                if (data.status === 200) {
                    window.location.href = '../../index.html'
                }
            })
        console.log(res)
    } catch (err) {
        console.log(err)
    }
});

const wrapper = document.querySelector("#wrapper");
const defaultBtn = document.querySelector("#default-btn");
const customBtn = document.querySelector("#custom-btn");
const img = document.querySelector("#productImg");
let regExp = /[0-9a-zA-Z\^\&\'\@\{\}\[\]\,\$\=\!\-\#\(\)\.\%\+\~\_ ]+$/;

function defaultBtnActive() {
    defaultBtn.click();
}

defaultBtn.addEventListener("change", function () {
    const file = this.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function () {
            const result = reader.result;
            img.src = result;
            wrapper.classList.add("active");
        }
        reader.readAsDataURL(file);
    }
    if (this.value) {
        let valueStore = this.value.match(regExp);
    }
});