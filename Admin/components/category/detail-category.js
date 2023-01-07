var url = 'https://localhost:7000/api/AdminCategory';

var categoryInfo = [];
var id = null;

const urlParams = new URLSearchParams(window.location.search);
id = urlParams.get('id');
const getCatedoryInfoUrl = url + "/" + id;

axios.get(getCatedoryInfoUrl)
    .then(res => {
        if (res.status === 200) {
            categoryInfo = res.data.data;
            var check = ``;

            if (categoryInfo.status === "Active") {
                check = `
                <tr>
                    <td>Status:</td>
                    <td>
                        <span class="badge badge-pill badge-success">Active</span>
                    </td>
                </tr>`
            } else {
                check = `
                <tr>
                    <td>Status:</td>
                    <td>
                        <span class="badge badge-pill badge-danger">In Active</span>
                    </td>
                </tr>`
            };


            var content = ` 
                <div class="page-header no-gutters has-tab">
                    <div class="d-md-flex m-b-15 align-items-center justify-content-between">
                        <div class="media align-items-center m-b-15">
                            <div class="page-header">
                                <h2 class="header-title">Category Detail</h2>
                                <div class="header-sub-title">
                                    <nav class="breadcrumb breadcrumb-dash">
                                        <a href="../index.html" class="breadcrumb-item"><i class="anticon anticon-home m-r-5"></i>Home</a>
                                        <a class="breadcrumb-item" href="#">Shop</a>
                                        <a class="breadcrumb-item" href="../category/category.html">Categories List</a>
                                        <span class="breadcrumb-item active">Category Detail</span>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                    <ul class="nav nav-tabs" >
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#category-overview">Overview</a>
                        </li>
                    </ul>
                </div>
                <div class="container-fluid">
                    <div class="tab-content m-t-15">
                        <div class="tab-pane fade show active" id="category-overview" >
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="card-title">Basic Info</h4>
                                    <div class="table-responsive">
                                        <table class="product-info-table m-t-20">
                                            <tbody>
                                            <tr>
                                                    <td>Code:</td>
                                                    <td>${categoryInfo.id}</td>
                                                </tr>
                                                <tr>
                                                    <td>Code:</td>
                                                    <td>${categoryInfo.code}</td>
                                                </tr>
                                                <tr>
                                                    <td>Name:</td>
                                                    <td class="text-dark font-weight-semibold">${categoryInfo.name}</td>
                                                </tr>
                                                <tr>
                                                    <td>Parent:</td>
                                                    <td>${categoryInfo.parentId}</td>
                                                </tr>
                                                <tr>
                                                    <td>Slug:</td>
                                                    <td>${categoryInfo.slug}</td>
                                                </tr>
                                                <tr>
                                                    <td>Description:</td>
                                                    <td>${categoryInfo.description}</td>
                                                </tr>
                                                `
                + check +
                `
                                            </tbody>
                                        </table> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            `;

            document.querySelector('#category-detail').innerHTML = content;
        }
    })