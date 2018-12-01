<template>
  <div id="app">
    <navbar></navbar>
    <div class="contentBody">
      <router-view/>
    </div>
  </div>
</template>

<script>
import Navbar from "./components/Navbar";
export default {
  components: {Navbar}
};
</script>


<style>
.contentBody{
  margin-top:25px
}
.mu-button.m-left {
  margin-left: 10px;
}
</style>
