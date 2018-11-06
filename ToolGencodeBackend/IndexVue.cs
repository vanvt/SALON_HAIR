using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public class IndexVue
    {
        static public string Template = @"
<template>
      <div class=""content-wrapper"">
    <!-- Content Header (Page header) -->
    <section class=""content-header"">
      <h1>
        Quản lý {ClassName}
      </h1>
      <ol class=""breadcrumb"">
        <li><nuxt-link to=""/""><i class=""fa fa-dashboard""></i> Home</nuxt-link></li>
        <li><nuxt-link to=""/account"">Quản lý tài khoản</nuxt-link></li>
      </ol>
    </section>
    <!-- Main content -->
    <section class=""content"">
      <div class=""row"">
        <create-{InstanceName} :{ClassName}=""{ClassName}"" @clearform=""clearform""/>
        <list-{InstanceName} @get{ClassName}=""get{ClassName}""/>
    </div>
    </section>
  </div>
</template>

<script>
import List{ClassName} from ""./list.vue"";
import Create{ClassName} from ""./create.vue"";
export default {
  layout: ""master"",
  data() {
    return {
      {ClassName}: {}
    };
  },
  components: {
    List{ClassName},
    Create{ClassName}
  },
  methods: {
	get{ClassName}({InstanceName}) {
      apibase        
      .get(this.$authToken, {InstanceName}.id, ""{InstanceName}s"")
        .then(rs => {
          this.{ClassName} = rs.data.data;
          // this.meta = rs.data.meta;
          //console.log(rs.data.data);
        })
        .catch(err => {
          console.log(err);
        });     
    },
    clearform(){
      this.{ClassName} = {};
    }
  }
};
</script>

<style>
</style>

";
    }
}
