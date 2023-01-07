var url = 'https://localhost:7000/api/AdminCategory?status=0';

const form = document.getElementById('form-validation');

form.addEventListener('submit', async function (e) {
    e.preventDefault();

    const code = document.getElementById('categoryCode').value;
    const name = document.getElementById('categoryName').value;
    const parentId = document.getElementById('parentId').value;
    console.log(parentId);
    const slug = document.getElementById('categorySlug').value;
    const status = document.getElementById('categoryStatus').value;
    const description = document.getElementById('categoryDescription').childNodes[0].childNodes[0].outerHTML;

    try {
        const res = await axios.post(url, {
            code: code,
            name: name,
            parentId: parentId,
            slug: slug,
            status: status,
            description: description,
        })
            .then(data => {
                if (data.status === 200) {
                    window.location.href = 'category.html'
                }
            })
        console.log(res)
    } catch (err) {
        console.log(err)
    }
});

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
            rangelength: "Enter 2 characters"
        }
    }
});

var quill = new Quill('#categoryDescription', {
    theme: 'snow'
});