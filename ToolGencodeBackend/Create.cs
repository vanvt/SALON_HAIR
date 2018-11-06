using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public class Create
    {
        public static string Template = @"
<template>
       <div class=""col-md-4"">
          <div class=""box box-default"">
            <div class=""box-header with-border"">
              <h3 class=""box-title"">Thêm mới & Chỉnh sửa</h3>
            </div>         
            <form>
              <div class=""box-body"">           
                {CreateForm}
              </div>
              <div class=""box-footer"">
                <button class=""btn btn-primary"" @click=""submit"" ><i class=""fa fa-save""></i> Lưu thay đổi</button>
                <a href=""#"" class=""btn btn-default"" style=""margin-left:20px;"" v-on:click.prevent=""clearForm()"">
                  <i class=""fa fa-undo""></i> Hủy
                </a>
              </div>
            </form>
          </div>
        </div>
</template>

<script>
import InputGroup from ""@/components/ui-kits/input-group"";
import apibase from ""@/assets/js/api/baseApi.js"";
import {
  required,
  minLenght,
  maxLength,
  requiredIf,
  requiredUnless,
  numeric
} from ""vuelidate/lib/validators"";
export default {
  props: ['{InstanceName}'],
  layout: ""master"",
  data() {
    return {
      
    };
  },
  components: {
    InputGroup
  },
  computed:{
    
  },
  methods:{
    clearForm(){
      this.$emit(""clearform"");
    },
    submit(){
      apibase.create(this.$authToken, this.{InstanceName}, ""{InstanceName}s"")
      .then(rs => {
          this.$emit(""reloadlist"")
        })
        .catch(err => {
          console.log(err);
        });

    }
  }
};
</script>

<style>
</style>

";

    }
}
