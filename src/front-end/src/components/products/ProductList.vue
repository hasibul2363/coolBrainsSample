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
        </div>

        <mu-data-table :loading="loading" no-data-text="..." :columns="columns" :data="products">
          <!-- <template slot-scope="scope">
            <td>{{scope.row.name}}</td>
            <td class="is-right">{{scope.row.calories}}</td>
            <td class="is-right">{{scope.row.fat}}</td>
            <td class="is-right">{{scope.row.carbs}}</td>
            <td class="is-right">{{scope.row.protein}}</td>
            <td class="is-right">{{scope.row.iron}}%</td>
          </template>-->
        </mu-data-table>
      </mu-paper>
    </mu-container>
  </div>
</template>
<script>
import productService from './ProductService'
export default {
  methods: {
    async getProducts() {
      var response = await productService.doProductQury(this.filterModel);
      this.products = response.data.data;
    },
    doExcelExport() {
      alert("Ex" + filterModel.name + filterModel.code);
    }
  },
  data() {
    return {
      filterModel: {
          name:"",
          code:"",
          pageNumber:1,
          pageSize:10
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
          title: "Action",
          name: "lastUpdated ",
          align: "center",
          sortable: false,
          width: 300
        }
      ],
      loading: false
    };
  }
};
</script>
<style>
</style>

