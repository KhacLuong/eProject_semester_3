var url = 'https://localhost:7000/api/AdminManufacturer';

var manufacturerInfo = [];
var id = null;

const urlParams = new URLSearchParams(window.location.search);
id = urlParams.get('id');
const getManufacturerInfoUrl = url + "/" + id;

axios.get(getManufacturerInfoUrl)
    .then(res => {
        if (res.status === 200) {
            manufacturerInfo = res.data.data;

            var content = `
                    <div class="page-header no-gutters has-tab">
                        <div class="d-md-flex m-b-15 align-items-center justify-content-between">
                            <div class="media align-items-center m-b-15">
                            <div class="page-header">
                            <h2 class="header-title">Manucfaturer Detail</h2>
                            <div class="header-sub-title">
                                <nav class="breadcrumb breadcrumb-dash">
                                    <a href="../index.html" class="breadcrumb-item"><i class="anticon anticon-home m-r-5"></i>Home</a>
                                    <a class="breadcrumb-item" href="#">Shop</a>
                                    <a class="breadcrumb-item" href="../manufacturer/manufacturer.html">Manufacturers List</a>
                                    <span class="breadcrumb-item active">Manufacturer Detail</span>
                                </nav>
                            </div>
                        </div>
                            </div>
                        </div>
                        <ul class="nav nav-tabs" >
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#manufacturer-overview">Overview</a>
                            </li>
                        </ul>
                    </div>
                    <div class="container-fluid">
                        <div class="tab-content m-t-15">
                            <div class="tab-pane fade show active" id="manufacturer-overview" >
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title">Basic Info</h4>
                                        <div class="table-responsive">
                                            <table class="product-info-table m-t-20">
                                                <tbody>
                                                    <tr>
                                                        <td>Id:</td>
                                                        <td>${manufacturerInfo.id}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Code:</td>
                                                        <td>${manufacturerInfo.code}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Name:</td>
                                                        <td class="text-dark font-weight-semibold">${manufacturerInfo.name}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Email:</td>
                                                        <td>${manufacturerInfo.email}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Phone Number:</td>
                                                        <td>${manufacturerInfo.phoneNumber}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Address:</td>
                                                        <td>${manufacturerInfo.address}</td>
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

            document.querySelector('#manufacturer-detail').innerHTML = content;
        }
    })