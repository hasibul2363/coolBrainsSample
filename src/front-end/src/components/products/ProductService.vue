<script>
import axios from "axios";
var serviceBaseUrl = "http://localhost:5000/api/";
export default {
  doProductQury(query) {
    return axios.post(serviceBaseUrl + "query/ProductQuery", query);
  },
  getDoExcelExportUrl(query) {
    return serviceBaseUrl + "query/DoExcelExport?name="+query.name+"&code="+query.code;
  },
  createProduct(model) {
    return axios.post(serviceBaseUrl + "command/CreateProduct", model);
  },
  deleteProduct(model) {
    return axios.post(serviceBaseUrl + "command/DeleteProduct", model);
  },
  updateProduct(model) {
    return axios.post(serviceBaseUrl + "command/UpdateProduct", model);
  },
  getGuid() {
    var d = new Date().getTime();
    if (
      typeof performance !== "undefined" &&
      typeof performance.now === "function"
    ) {
      d += performance.now();
    }
    var newGuid = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
      /[xy]/g,
      function(c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c === "x" ? r : (r & 0x3) | 0x8).toString(16);
      }
    );
    return newGuid;
  }
};
</script>
