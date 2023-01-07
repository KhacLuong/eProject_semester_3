var url = 'https://localhost:7000/api/AdminBlog';

const form = document.getElementById('form-validation');

form.addEventListener('submit', async function (e) {
    e.preventDefault();

    const image = document.getElementById('productImg').src;
    const title = document.getElementById('blogTitle').value;
    //const author = document.getElementById('authorId').value;
    const slug = document.getElementById('blogSlug').value;
    const status = document.getElementById('blogStatus').value;
    const content = document.getElementById('blogContent').childNodes[0].childNodes[0].outerHTML;


    try {
        const res = await axios.post(url, {
            avater: image,
            title: title,
            //author: author,
            slug: slug,
            status: status,
            content: content,
        })
            .then(data => {
                if (data.status === 200) {
                    window.location.href = 'blog.html'
                }
            })
        console.log(res)
    } catch (err) {
        console.log(err)
    }
});

var quill = new Quill('#blogContent', {
    theme: 'snow'
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