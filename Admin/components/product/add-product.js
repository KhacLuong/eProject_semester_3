var url = 'https://localhost:7000/api/AdminProduct';

const form = document.getElementById('form-validation');

form.addEventListener('submit', async function (e) {
    e.preventDefault();
    const image = document.getElementById('productImg').src;
    const imgName = document.getElementById('file-name').textContent;
    const code = document.getElementById('productCode').value;
    const code1 = document.getElementById('code1').textContent;
    const code2 = document.getElementById('code2').textContent;
    const productCode = code1 + code2 + code;
    const name = document.getElementById('productName').value;
    const intro = document.getElementById('productIntro').value;
    const categoryId = document.getElementById('categoryId').value.split(',')[0];
    const manufacturerId = document.getElementById('manufacturerId').value.split(',')[0];
    const authorId = document.getElementById('authorId').value;
    const price = document.getElementById('productPrice').value;
    const quantity = document.getElementById('productQuantity').value;
    const slug = document.getElementById('productSlug').value;
    const status = document.getElementById('productStatus').value;
    const description = document.getElementById('productDescription').childNodes[0].childNodes[0].outerHTML;

    try {
        const res = await axios.post(url, {
            imageProductPath: image,
            imageProductName: imgName,
            code: productCode,
            name: name,
            intro: intro,
            categoryId: categoryId,
            manufacturerId: manufacturerId,
            authorId: authorId,
            price: price,
            quantity: quantity,
            slug: slug,
            status: status,
            description: description
        })
            .then(data => {
                if (data.status === 200) {
                    window.location.href = 'product.html'
                }
            })
        console.log(res)
    } catch (err) {
        console.log(err)
    }
});

function productCode1(e) {
    var valueTwo = $('#categoryId').val().split(',')[1];
    document.getElementById("code1").innerHTML = valueTwo;

    /*
    if (valueTwo == "C2") {
        var content = `
            <label for="authorId" class="font-weight-semibold">Author</label>
            <select id="authorId" class="form-control" name="authorId" placeholder="Author">
            </select>
        `;
        document.querySelector('#test').innerHTML = content;
    }
    */
};

function productCode2(e) {
    var valueTwo = $('#manufacturerId').val().split(',')[1];
    document.getElementById("code2").innerHTML = valueTwo
};

axios.get('https://localhost:7000/api/AdminCategory?status=0&pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#categoryId').innerHTML = '';
            var categories = response.data.data;
            var content = ``;
            var empty = `<option></option>`;
            categories.map(category => {
                content += `<option value="${category.id},${category.code}">${category.name}</option>`;
            });
            document.querySelector('#categoryId').innerHTML = empty + content;
        }
    });

axios.get('https://localhost:7000/api/AdminManufacturer?pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#manufacturerId').innerHTML = '';
            var manufacturers = response.data.data.manufacturers;
            var content = ``;
            var empty = `<option></option>`;
            manufacturers.map(manufacturer => {
                content += `<option value="${manufacturer.id},${manufacturer.code}">${manufacturer.name}</option>`;
            });
            document.querySelector('#manufacturerId').innerHTML = empty + content;
        }
    });

axios.get('https://localhost:7000/api/AdminAuthor?pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#authorId').innerHTML = '';
            var authors = response.data.data.authors;
            var content = ``;
            var empty = `<option></option>`;
            authors.map(author => {
                content += `<option value="${author.id}">${author.name}</option>`;
            });
            document.querySelector('#authorId').innerHTML = empty + content;
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
        categoryId: {
            required: true
        },
        manufacturerId: {
            required: true
        },
        price: {
            required: true,
            number: true
        },
        quantity: {
            required: true,
            number: true
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
        },
        price: {
            number: "Enter numbers only",
        },
        quantity: {
            number: "Enter numbers only",
        },
    }
});


var quill = new Quill('#productDescription', {
    theme: 'snow'
});

const wrapper = document.querySelector("#wrapper");
const fileName = document.querySelector("#file-name");
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
        fileName.textContent = valueStore;
    }
});