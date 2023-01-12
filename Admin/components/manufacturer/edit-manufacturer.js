var url = 'https://localhost:7000/api/AdminManufacturer';

var manufacturerInfo = [];
var id = null;

function getManufacturerInfo() {
    const urlParams = new URLSearchParams(window.location.search);
    id = urlParams.get('id');
    const getManufacturerInfoUrl = url + "/" + id;
    axios.get(getManufacturerInfoUrl)
        .then(res => {
            if (res.status === 200) {
                manufacturerInfo = res.data.data;
                console.log(manufacturerInfo);
                document.querySelector('[name="code"]').value = manufacturerInfo.code;
                document.querySelector('[name="name"]').value = manufacturerInfo.name;
                document.querySelector('[name="email"]').value = manufacturerInfo.email;
                document.querySelector('[name="phoneNumber"]').value = manufacturerInfo.phoneNumber;
                document.querySelector('[name="address"]').value = manufacturerInfo.address;
            }
        })
}

function editManufacturer() {
    const code = document.querySelector('[name="code"]').value;
    const name = document.querySelector('[name="name"]').value;
    const email = document.querySelector('[name="email"]').value;
    const phoneNumber = document.querySelector('[name="phoneNumber"]').value;
    const address = document.querySelector('[name="address"]').value;

    manufacturerInfo.code = code;
    manufacturerInfo.name = name;
    manufacturerInfo.email = email;
    manufacturerInfo.phoneNumber = phoneNumber;
    manufacturerInfo.address = address;

    const editManufacturerInfoUrl = url + "/" + id;

    axios.put(editManufacturerInfoUrl, manufacturerInfo)
        .then(res => {
            if (res.status === 200) {
                window.location.href = "manufacturer.html";
            }
        })
    return false;
};

$("#form-validation").validate({
    errorElement: 'div',
    errorClass: 'is-invalid',
    validClass: 'is-valid',
    rules: {
        code: {
            required: true,
            rangelength: [3, 3]
        },
        name: {
            required: true
        },
        email: {
            required: true
        },
        phoneNumber: {
            required: true,
            number: true,
            rangelength: [10, 10]
        },
        address: {
            required: true
        }
    },
    messages: {
        code: {
            rangelength: "Enter 3 characters"
        },
        phoneNumber: {
            number: "Enter numbers only",
            rangelength: "Enter 10 characters"
        },
    }
});