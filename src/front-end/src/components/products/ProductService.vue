<script>
import axios from "axios";
var serviceBaseUrl = "http://localhost:5000/api/";
export default {
  doProductQury: query => {
    return new Promise((resolve, reject) => {
      axios
        .post(serviceBaseUrl + "query/ProductQuery", query)
        .then(response => resolve(response.data))
        .catch(e => reject(e));
    });
  }
};
</script>
