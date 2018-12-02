<template>
  <div>
    <mu-container>
      <mu-card style="width: 100%;  margin: 0 auto;">
        <mu-card-title title="Product List" sub-title></mu-card-title>
        <mu-card-text>
          <mu-row gutter>
            <mu-col span="6">
              <mu-text-field
                full-width
                v-model="filterModel.name"
                placeholder="Search by product name"
                @keyup.enter="getProducts"
              ></mu-text-field>
            </mu-col>
            <mu-col span="6">
              <mu-text-field
                full-width
                v-model="filterModel.code"
                placeholder="Search by product code"
                @keyup.enter="getProducts"
              ></mu-text-field>
            </mu-col>
          </mu-row>
          <mu-row gutter>
            <mu-col span="6">
              <mu-button full-width @click="getProducts">
                <mu-icon value="search"></mu-icon>Search
              </mu-button>
            </mu-col>
            <mu-col span="6">
              <mu-button full-width @click="doExcelExport">
                <mu-icon value="arrow_downward"></mu-icon>Export
              </mu-button>
            </mu-col>
          </mu-row>

          <mu-button large fab color="red" @click="addProduct" class="btn-add">
            <mu-icon value="add"></mu-icon>
          </mu-button>

          <br>

          <mu-data-table :loading="loading" no-data-text="..." :columns="columns" :data="products">
            <template slot-scope="scope">
              <td>
                <img :src="scope.row.photoUrl" class="productimg" alt>
              </td>
              <td class="is-center">{{scope.row.code}}</td>
              <td class="is-center">{{scope.row.name}}</td>
              <td class="is-center">{{scope.row.price}}</td>
              <td class="is-center">{{scope.row.lastUpdated|date}}</td>
              <td class="is-right">
                <mu-button small flat @click="edit(scope.row.id)">
                  <mu-icon value="edit"></mu-icon>
                </mu-button>
                <mu-button
                  small
                  flat
                  color="secondary"
                  @click="lastSelectedItemId = scope.row.id;openDeleteModal = true"
                >
                  <mu-icon value="delete"></mu-icon>
                </mu-button>
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
        </mu-card-text>
        <mu-card-actions></mu-card-actions>
      </mu-card>
      <mu-paper :z-depth="1"></mu-paper>
    </mu-container>
     <mu-snackbar position="top-end" :open="willShow" :color="noticationColor">
        {{noticationMessage}}
        <mu-button flat slot="action" @click="willShow = false" color="#fff">Close</mu-button>
      </mu-snackbar>
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
    async doExcelExport() {
      let link = document.createElement("a");
      link.href = productService.getDoExcelExportUrl(this.filterModel);
      link.click();
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
        this.noticationColor = "success";
        this.noticationMessage = "Action Successfull";
        this.willShow = true;
        setTimeout(() => {
          this.willShow = false;
        }, 3000);
      } else {
        this.noticationMessage = "Failed to delete";
        this.willShow = true;
        this.noticationColor = "error";
        setTimeout(() => {
          this.willShow = false;
        }, 5000);
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
          sortable: false,
          width: 240
        },
        {
          title: "",
          sortable: false,
          width: 225
        }
      ],
      loading: false,
      openDeleteModal: false,
      lastSelectedItemId: "",
      productCount: 0,
      noticationMessage: "",
      willShow: false,
      noticationColor: "success",
    };
  },
  created() {
    this.getProducts(true);
  }
};
</script>
<style>
img.productimg {
  width: 100px;
  height: 80px;
}

.pd-10 {
  padding: 10px;
}
.btn-add {
  outline: none;
  -webkit-appearance: none;
  position: fixed;
  right: 16px;
  bottom: 16px;
  z-index: 101;
}
.txt-right {
  text-align: right;
}
.title {
  background-color: #2196f3;
  padding: 10px;
  color: #fff;
}
</style>

