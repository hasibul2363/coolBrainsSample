<template>
  <div class="hello">
    <mu-appbar style="width: 100%;" color="primary" title="SuitSupply">
      <mu-button flat slot="right" @click="navigateToHome">Home</mu-button>
      <mu-button flat slot="right" @click="navigateToProducts">Products</mu-button>
    </mu-appbar>
  </div>
</template>

<script>
import router from "../router";
export default {
  name: "Navbar",
  methods: {
    navigateToHome: () => {
      router.push({ path: "/" });
    },
    navigateToProducts: () => {
      router.push({ path: "/products" });
    }
  }
};
</script>