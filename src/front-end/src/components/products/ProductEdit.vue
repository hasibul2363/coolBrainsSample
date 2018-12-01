<template>
  <div>
    <mu-container>
      <mu-snackbar position="top-end" :open="willShow" :color="noticationColor">
        {{noticationMessage}}
        <mu-button flat slot="action" @click="willShow = false" color="#fff">Close</mu-button>
      </mu-snackbar>
      <mu-card style="width: 100%;  margin: 0 auto;">
        <mu-card-title title="Update Product" sub-title></mu-card-title>
        <mu-card-text>
          <mu-form :model="product" class="mu-demo-form" label-width="100" ref="form">
            <mu-form-item prop="code" label="Code" :rules="validationRules.code">
              <mu-text-field disabled v-model="product.code"></mu-text-field>
            </mu-form-item>
            <mu-form-item label="Name" help-text prop="name" :rules="validationRules.name">
              <mu-text-field v-model="product.name" prop="name"></mu-text-field>
            </mu-form-item>
            <mu-form-item prop="photoUrl" label="Photo Url" :rules="validationRules.photoUrl">
              <mu-text-field v-model="product.photoUrl"></mu-text-field>
            </mu-form-item>
            <mu-form-item type="number" prop="price" label="Price" :rules="validationRules.price">
              <mu-text-field v-model="product.price"></mu-text-field>
            </mu-form-item>
          </mu-form>
        </mu-card-text>
        <mu-card-actions>
          <mu-button color="primary" @click="save">Save</mu-button>
          <mu-button class="m-left" @click="cancel">Cancel</mu-button>
        </mu-card-actions>
      </mu-card>
      <mu-dialog
        width="600"
        max-width="80%"
        title="Confirmation!"
        :esc-press-close="false"
        :overlay-close="false"
        :open.sync="openModal"
      >Do you want set price greater than 999?
        <mu-button
          slot="actions"
          color="primary"
          @click="greaterThan999Confirmed = true; save()"
        >Yes</mu-button>
        <mu-button
          slot="actions"
          @click="greaterThan999Confirmed = false; openModal = false;"
        >Cancel</mu-button>
      </mu-dialog>
    </mu-container>
  </div>
</template>

<script>
import productService from "./ProductService";
import router from "../../router";
function buildValidationRules() {
  var urlRe = /^(http[s]?:\/\/){0,1}(www\.){0,1}[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,5}[\.]{0,1}/;
  var notEmpty = { validate: val => !!val, message: "Cannot be empty" };
  var number = { validate: val => val > 0, message: "must be greater than 0" };
  var urlValidator = {
    validate: val => {
      if (!val) return true;
      return urlRe.test(val);
    },
    message: "Url not valid"
  };
  return {
    code: [notEmpty],
    name: [notEmpty],
    price: [number],
    photoUrl: [urlValidator]
  };
}
export default {
  data() {
    var emptyProduct = {
      id: productService.getGuid(),
      code: "",
      name: "",
      photoUrl: "",
      price: 0.0
    };
    return {
      product: emptyProduct,
      validationRules: buildValidationRules(),

      noticationMessage: "",
      willShow: false,
      noticationColor: "success",
      greaterThan999Confirmed: false,
      openModal: false
    };
  },
  methods: {
    buildErrorMessage(response) {
      var message = [];
      response.data.validationResult.errors.forEach(error => {
        message.push(error.errorMessage);
      });
      return message.join(",");
    },
    async save() {
      this.openModal = false;
      var validationResult = await this.$refs.form.validate();
      if (!validationResult) return;
      if (this.product.price > 999 && !this.greaterThan999Confirmed) {
        this.openModal = true;
        return;
      }

      var response = await productService.updateProduct(this.product);

      if (response.data.success) {
        this.noticationColor = "success";
        this.noticationMessage = "Action Successfull";
        this.willShow = true;
        setTimeout(() => {
          this.willShow = false;
        }, 3000);
      } else {
        this.noticationMessage = this.buildErrorMessage(response);
        console.log(this.noticationMessage);
        this.willShow = true;
        this.noticationColor = "error";
        setTimeout(() => {
          this.willShow = false;
        }, 5000);
      }
    },
    cancel() {
      router.push({ path: "/products" });
    }
  },
  async created() {
    var productId = this.$route.params.id;
    var response = await productService.doProductQury({
      id: productId,
      pageNumber: 1,
      pageSize: 1
    });
    this.product = response.data.data[0];
  }
};
</script>
