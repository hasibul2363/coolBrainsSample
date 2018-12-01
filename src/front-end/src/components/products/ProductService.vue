<script>
import axios from "axios";
var serviceBaseUrl = "http://localhost:5000/api/";
export default {
  doProductQury: query => {
    return axios.post(serviceBaseUrl + "query/ProductQuery", query);
  }
};
</script>
