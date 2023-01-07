var url = 'https://localhost:7000/api/AdminProduct';

var productInfo = [];
var id = null;

const urlParams = new URLSearchParams(window.location.search);
id = urlParams.get('id');
const getProductInfoUrl = url + "/" + id;

axios.get(getProductInfoUrl)
    .then(res => {
        if (res.status === 200) {
            productInfo = res.data.data;
            var check = ``;

            if (productInfo.status === "Active") {
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
            <div>
            <div class="page-header">
                                    <h2 class="header-title">Product Detail</h2>
                                    <div class="header-sub-title">
                                        <nav class="breadcrumb breadcrumb-dash">
                                            <a href="../index.html" class="breadcrumb-item"><i class="anticon anticon-home m-r-5"></i>Home</a>
                                            <a class="breadcrumb-item" href="#">Shop</a>
                                            <a class="breadcrumb-item" href="../product/product.html">Products List</a>
                                            <span class="breadcrumb-item active">Product Detail</span>
                                        </nav>
                                    </div>
                                </div>
            </div>
            <div class="d-md-flex m-b-15 align-items-center justify-content-between">
                <div class="media align-items-center m-b-15">
                    <div class="avatar avatar-image rounded" style="height: 70px; width: 70px">
                        <img src="${productInfo.imageProductPath}" alt="">
                    </div>
                    <div class="m-l-15">
                        <h4 class="m-b-0">${productInfo.name}</h4>
                        <p class="text-muted m-b-0">Code: ${productInfo.code}</p>
                    </div>
                </div>
                <div class="m-b-15">
                    <button class="btn btn-primary">
                        <i class="anticon anticon-edit"></i>
                        <span>Edit</span>
                    </button>
                </div>
            </div>
            <ul class="nav nav-tabs" >
                <li class="nav-item">
                    <a class="nav-link active" data-toggle="tab" href="#product-overview">Overview</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-toggle="tab" href="#product-images">Product Images</a>
                </li>
            </ul>
        </div>
        <div class="container-fluid">
            <div class="tab-content m-t-15">
                <div class="tab-pane fade show active" id="product-overview" >
                    <div class="row">
                        <div class="col-md-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="media align-items-center">
                                        <i class="font-size-40 text-success anticon anticon-smile"></i>
                                        <div class="m-l-15">
                                            <p class="m-b-0 text-muted">10 ratings</p>
                                            <div class="star-rating m-t-5">
                                                <input type="radio" id="star3-5" name="rating-3" value="5" checked disabled/><label for="star3-5" title="5 star"></label>
                                                <input type="radio" id="star3-4" name="rating-3" value="4" disabled/><label for="star3-4" title="4 star"></label>
                                                <input type="radio" id="star3-3" name="rating-3" value="3" disabled/><label for="star3-3" title="3 star"></label>
                                                <input type="radio" id="star3-2" name="rating-3" value="2" disabled/><label for="star3-2" title="2 star"></label>
                                                <input type="radio" id="star3-1" name="rating-3" value="1" disabled/><label for="star3-1" title="1 star"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="media align-items-center">
                                        <i class="font-size-40 text-primary anticon anticon-shopping-cart"></i>
                                        <div class="m-l-15">
                                            <p class="m-b-0 text-muted">Sales</p>
                                            <h3 class="m-b-0 ls-1">1,521</h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="media align-items-center">
                                        <i class="font-size-40 text-primary anticon anticon-message"></i>
                                        <div class="m-l-15">
                                            <p class="m-b-0 text-muted">Reviews</p>
                                            <h3 class="m-b-0 ls-1">27</h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="media align-items-center">
                                        <i class="font-size-40 text-primary anticon anticon-stock"></i>
                                        <div class="m-l-15">
                                            <p class="m-b-0 text-muted">Available Stock</p>
                                            <h3 class="m-b-0 ls-1">${productInfo.quantity}</h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <h4 class="card-title">Basic Info</h4>
                            <div class="table-responsive">
                                <table class="product-info-table m-t-20">
                                    <tbody>
                                        <tr>
                                            <td>Id:</td>
                                            <td>${productInfo.id}</td>
                                        </tr>
                                        <tr>
                                            <td>Intro:</td>
                                            <td class="text-dark font-weight-semibold">${productInfo.intro}</td>
                                        </tr>
                                        <tr>
                                            <td>Category:</td>
                                            <td>${productInfo.category}</td>
                                        </tr>
                                        <tr>
                                            <td>Manufacturer:</td>
                                            <td>${productInfo.manufacturer}</td>
                                        </tr>
                                        <tr>
                                            <td>Author:</td>
                                            <td>${productInfo.author}</td>
                                        </tr>
                                        <tr>
                                            <td>Price:</td>
                                            <td>${productInfo.price} $</td>
                                        </tr>
                                        <tr>
                                            <td>Slug:</td>
                                            <td>${productInfo.slug}</td>
                                        </tr>`
                + check +
                `
                                    </tbody>
                                </table> 
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h4 class="card-title">Product Description</h4>
                        </div>
                        <div class="card-body">
                            ${productInfo.description}
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="product-images">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <img class="img-fluid" src="../../assets/images/others/product-1.jpg" alt="">
                                </div>
                                <div class="col-md-3">
                                    <img class="img-fluid" src="../../assets/images/others/product-2.jpg" alt="">
                                </div>
                                <div class="col-md-3">
                                    <img class="img-fluid" src="../../assets/images/others/product-3.jpg" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            `;

            document.querySelector('#product-detail').innerHTML = content;
        }
    })