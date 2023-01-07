var url = 'https://localhost:7000/api/AdminBlog';

axios.get(url)
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#list-blogs').innerHTML = '';
            var blogs = response.data.data.products;
            console.log(blogs);
            var content = ``;

            blogs.map(blog => {
                content += `
                <div class="col-md-3">
                    <div class="card">
                        <img class="card-img-top" src="${blog.avatar}" alt="">
                        <div class="card-body">
                            <h4 class="m-t-10">${blog.title}</h4>
                            <p class="m-b-20">${blog.content}</p>
                            <div class="d-flex align-items-center justify-content-between">
                                <p class="m-b-0 text-dark font-weight-semibold font-size-15">${blog.createdAt}</p>
                                <a class="text-primary btn btn-sm btn-hover" href="blog-detail.html">
                                    <span>Read More</span>
                                </a>
                            </div>
                        </div>
                        <div class="d-flex justify-content-between card-footer">
                            <div class="d-flex align-items-center m-t-5">
                                <div class="avatar avatar-image avatar-sm">
                                    <img src="../../assets/images/avatars/thumb-5.jpg" alt="">
                                </div>
                                <div class="m-l-10">
                                    <span class="font-weight-semibold">Nicole Wyne</span>
                                </div>
                            </div>
                            <div class="text-right m-t-5">
                                <button href="" class="btn btn-icon btn-hover btn-sm btn-rounded pull-right">
                                    <i class="anticon anticon-edit"></i>
                                </button>
                                <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="removeCategory(${blog.id})">
                                    <i class="anticon anticon-delete"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            });
            document.querySelector('#list-blogs').innerHTML = content;
        }
    });