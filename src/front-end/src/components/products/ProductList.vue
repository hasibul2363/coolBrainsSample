<template>
  <div>
    <h2>Product List</h2>
    <mu-container>
      <mu-paper :z-depth="1">
        <mu-text-field v-model="filterModel.name" placeholder="Search by product name"></mu-text-field>
        <mu-text-field v-model="filterModel.code" placeholder="Search by product code"></mu-text-field>
        <div class="button-wrapper">
          <mu-button flat @click="getProducts">Search</mu-button>
          <mu-button flat @click="doExcelExport">Export</mu-button>
          <mu-button flat @click="addProduct">Add Product</mu-button>
        </div>

        <mu-data-table :loading="loading" no-data-text="..." :columns="columns" :data="products">
          <template slot-scope="scope">
            <td>
              <img :src="scope.row.photoUrl" class="img-fluid productimg" alt>
            </td>
            <td class="is-center">{{scope.row.code}}</td>
            <td class="is-center">{{scope.row.name}}</td>
            <td class="is-center">{{scope.row.price}}</td>
            <td class="is-center">{{scope.row.lastUpdated}}</td>
            <td class="is-right">
              <mu-button small flat @click="edit(scope.row.id)">Edit</mu-button>
              <mu-button
                small
                flat
                color="secondary"
                @click="lastSelectedItemId = scope.row.id;openDeleteModal = true"
              >Remove</mu-button>
              <mu-dialog
                width="600"
                max-width="80%"
                title="Confirmation!"
                :esc-press-close="false"
                :overlay-close="false"
                :open.sync="openDeleteModal"
              >Do you want to delete this product?
                <mu-button slot="actions" color="primary" @click="doDelete">Yes</mu-button>
                <mu-button slot="actions" @click="openDeleteModal = false">Cancel</mu-button>
              </mu-dialog>
            </td>
          </template>
        </mu-data-table>
        <mu-pagination
          :total="productCount"
          @change="onPageChange"
          :page-size="filterModel.pageSize"
          :current.sync="filterModel.pageNumber"
        ></mu-pagination>
      </mu-paper>
    </mu-container>
  </div>
</template>
<script>
import productService from "./ProductService";
import router from "../../router";
export default {
  methods: {
    async getProducts(reset) {
      if (reset) {
        this.filterModel.pageNumber = 1;
      }
      var response = await productService.doProductQury(this.filterModel);
      this.products = response.data.data;
      this.productCount = response.data.totalCount;
    },
    doExcelExport() {
      alert("Ex" + filterModel.name + filterModel.code);
    },
    addProduct() {
      router.push({ path: "/productcreate" });
    },
    async doDelete() {
      this.openDeleteModal = false;
      var response = await productService.deleteProduct({
        id: this.lastSelectedItemId
      });
      if (response.data.success) {
        this.getProducts(true);
      }
    },
    edit(id) {
      router.push({ path: "/productedit/" + id });
    },
    onPageChange(pageNumber) {
      this.filterModel.pageNumber = pageNumber;
      this.getProducts(false);
    }
  },
  data() {
    return {
      filterModel: {
        name: "",
        code: "",
        pageNumber: 1,
        pageSize: 3
      },
      products: [],
      columns: [
        {
          title: "Photo",
          name: "photoUrl",
          align: "center",
          sortable: false
        },
        {
          title: "Code",
          name: "code",
          align: "center",
          sortable: false
        },
        {
          title: "Name",
          name: "name",
          align: "center",
          sortable: false
        },
        {
          title: "Price",
          name: "price",
          align: "center",
          sortable: false
        },
        {
          title: "LastUpdated",
          name: "lastUpdated",
          align: "center",
          sortable: false
        },
        {
          title: "",
          sortable: false,
          width: 300
        }
      ],
      loading: false,
      openDeleteModal: false,
      lastSelectedItemId: "",
      productCount: 0
    };
  },
  created() {
    this.getProducts(true);
  }
};
</script>
<style>
img.productimg {
  width: 150px;
  height: 100px;
}
</style>

