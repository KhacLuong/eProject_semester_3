var url = 'https://localhost:7000/api/AdminManufacturer';

const form = document.getElementById('form-validation');

form.addEventListener('submit', async function (e) {
    e.preventDefault();

    const code = document.getElementById('manufacturerCode').value;
    const name = document.getElementById('manufacturerName').value;
    const email = document.getElementById('manufacturerEmail').value;
    const phoneNumber = document.getElementById('manufacturerPhone').value;
    const address = document.getElementById('manufacturerAddress').value;

    try {
        const res = await axios.post(url, {
            code: code,
            name: name,
            email: email,
            phoneNumber: phoneNumber,
            address: address,
        })
            .then(data => {
                if (data.status === 200) {
                    window.location.href = 'manufacturer.html'
                }
            })
        console.log(res)
    } catch (err) {
        console.log(err)
    }
});

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
            required: true,
            email: true
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