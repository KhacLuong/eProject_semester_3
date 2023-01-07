var url = 'https://localhost:7000/api/AdminAuthor';

const form = document.getElementById('form-validation');

form.addEventListener('submit', async function (e) {
    e.preventDefault();

    const name = document.getElementById('authorName').value;
    const email = document.getElementById('authorEmail').value;
    const phone = document.getElementById('authorPhone').value;

    try {
        const res = await axios.post(url, {
            name: name,
            email: email,
            phone: phone,
        })
            .then(data => {
                if (data.status === 200) {
                    window.location.href = 'author.html'
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
        name: {
            required: true
        },
        email: {
            required: true,
            email: true
        },
        phone: {
            required: true,
            rangelength: [10, 10]
        },
    },
    messages: {
        phone: {
            rangelength: "Enter 10 characters"
        },
    }
});