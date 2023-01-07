var url = 'https://localhost:7000/api/AdminAuthor';

var authorInfo = [];
var id = null;

function getAuthorInfo() {
    const urlParams = new URLSearchParams(window.location.search);
    id = urlParams.get('id');
    const getAuthorInfoUrl = url + "/" + id;
    axios.get(getAuthorInfoUrl)
        .then(res => {
            if (res.status === 200) {
                authorInfo = res.data.data;
                document.querySelector('[name="name"]').value = authorInfo.name;
                document.querySelector('[name="email"]').value = authorInfo.email;
                document.querySelector('[name="phoneNumber"]').value = authorInfo.phone;
            }
        })
}

function editAuthor() {
    const name = document.querySelector('[name="name"]').value;
    const email = document.querySelector('[name="email"]').value;
    const phone = document.querySelector('[name="phoneNumber"]').value;

    authorInfo.name = name;
    authorInfo.email = email;
    authorInfo.phone = phone;

    const editAuthorInfoUrl = url + "/" + id;

    axios.put(editAuthorInfoUrl, authorInfo)
        .then(res => {
            if (res.status === 200) {
                window.location.href = "author.html";
            }
        })
    return false;
};

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
        phoneNumber: {
            required: true,
            rangelength: [10, 10]
        },
    },
    messages: {
        phoneNumber: {
            rangelength: "Enter 10 characters"
        },
    }
});