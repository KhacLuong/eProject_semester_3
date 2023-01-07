var url = 'https://localhost:7000/api/AdminAuthor';

var authorInfo = [];
var id = null;

const urlParams = new URLSearchParams(window.location.search);
id = urlParams.get('id');
const getAuthorInfoUrl = url + "/" + id;

axios.get(getAuthorInfoUrl)
    .then(res => {
        if (res.status === 200) {
            authorInfo = res.data.data;

            var content = ` 
                <div class="page-header no-gutters has-tab">
                    <div class="d-md-flex m-b-15 align-items-center justify-content-between">
                        <div class="media align-items-center m-b-15">
                            <div class="page-header">
                                <h2 class="header-title">Author Detail</h2>
                                <div class="header-sub-title">
                                    <nav class="breadcrumb breadcrumb-dash">
                                        <a href="../index.html" class="breadcrumb-item"><i class="anticon anticon-home m-r-5"></i>Home</a>
                                        <a class="breadcrumb-item" href="#">Shop</a>
                                        <a class="breadcrumb-item" href="../author/author.html">Authors List</a>
                                        <span class="breadcrumb-item active">Author Detail</span>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                    <ul class="nav nav-tabs" >
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#product-overview">Overview</a>
                        </li>
                    </ul>
                </div>
                    <div class="container-fluid">
                        <div class="tab-content m-t-15">
                            <div class="tab-pane fade show active" id="author-overview" >
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title">Basic Info</h4>
                                        <div class="table-responsive">
                                            <table class="product-info-table m-t-20">
                                                <tbody>
                                                <tr>
                                                <td>Id:</td>
                                                <td>${authorInfo.id}</td>
                                            </tr>
                                                    <tr>
                                                        <td>Name:</td>
                                                        <td class="text-dark font-weight-semibold">${authorInfo.name}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Email:</td>
                                                        <td>${authorInfo.email}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Phone Number:</td>
                                                        <td>${authorInfo.phone}</td>
                                                    </tr>
                                                </tbody>
                                            </table> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            `;

            document.querySelector('#author-detail').innerHTML = content;
        }
    })