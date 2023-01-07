var url = 'https://localhost:7000/api/AdminCategory';

var categoryInfo = [];
var id = null;

function getCategoryInfo() {
    const urlParams = new URLSearchParams(window.location.search);
    id = urlParams.get('id');
    const getCategoryInfoUrl = url + "/" + id;
    axios.get(getCategoryInfoUrl)
        .then(res => {
            if (res.status === 200) {
                categoryInfo = res.data.data;

                document.querySelector('[name="code"]').value = categoryInfo.code;
                document.querySelector('[name="name"]').value = categoryInfo.name;
                document.querySelector('[name="slug"]').value = categoryInfo.slug;
                document.querySelector('[name="status"]').value = categoryInfo.status;
                document.querySelector('[name="parentId"]').value = categoryInfo.parentId;
                document.getElementById('categoryDescription').childNodes[0].childNodes[0].outerHTML = categoryInfo.description;
            }
        })
};

function editCategory() {
    const code = document.querySelector('[name="code"]').value;
    const name = document.querySelector('[name="name"]').value;
    const slug = document.querySelector('[name="slug"]').value;
    const status = document.querySelector('[name="status"]').value;
    const parentId = document.querySelector('[name="parentId"]').value;
    const description = document.getElementById('categoryDescription').childNodes[0].childNodes[0].outerHTML;
    const createdAt = "1/7/2023 3:05:19 PM";

    categoryInfo.code = code;
    categoryInfo.name = name;
    categoryInfo.slug = slug;
    categoryInfo.status = status;
    categoryInfo.parentId = parentId;
    categoryInfo.description = description;
    categoryInfo.createdAt = createdAt;

    const editCategoryInfoUrl = url + "/" + id;
    axios.put(editCategoryInfoUrl, categoryInfo)
        .then(res => {
            if (res.status === 200) {
                window.location.href = "category.html";
            }
        })
    return false;
};

axios.get('https://localhost:7000/api/AdminCategory?status=0&pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#parentId').innerHTML = '';
            var categories = response.data.data;
            var content = ``;
            var empty = `<option></option>`;
            categories.forEach(category => {
                content += `<option value="${category.id}">${category.name}</option>`;
            });
            document.querySelector('#parentId').innerHTML = empty + content;
        }
    });

$("#form-validation").validate({
    errorElement: 'div',
    errorClass: 'is-invalid',
    validClass: 'is-valid',
    rules: {
        code: {
            required: true,
            rangelength: [2, 2]
        },
        name: {
            required: true
        },
        slug: {
            required: true
        },
        status: {
            required: true
        },
    },
    messages: {
        code: {
            rangelength: "Enter 2 characters",
        }
    }
});

var quill = new Quill('#categoryDescription', {
    theme: 'snow'
});