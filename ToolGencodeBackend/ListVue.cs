using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public class ListVue
    {
      static string Tempalte = @"
<template>
      <div class=""col-md-8"">
          <div class=""box box-default"">
            <div class=""box-header with-border"">
              <h3 class=""box-title"">Danh sách {Classname} trên hệ thống</h3>
            </div>
            <div class=""box-body"">
              <div class=""form-group"">
                <div class=""row"">
                  <form>
                    <div class=""col-md-4"">
                      <input class=""form-control input-sm"" id=""keyword"" v-model=""query.keyword""  placeholder=""Từ khoá tìm kiếm"" type=""text"" value="""">
                    </div>
                    <div class=""col-md-2"">
                      <div>
                        <span class=""btn btn-primary btn-sm btn-block"" @click=""Gets()""><i class=""fa fa-search"" ></i> Tìm kiềm</span>
                        <div class=""clear-fix""></div>
                      </div>
                    </div>
                  </form>
                </div>
              </div>
              <table class=""table table-striped table-condensed"">
                <thead>
                  <tr>                
                   {HeaderProperties}
                  </tr>
                </thead>
                <tbody>
                  <tr v-for=""item in {InstanceName}s"" :key=""item.id"">
                    <td><a href=""#"" v-on:click.prevent=""load(item)"">{{item.name}}</a></td>
					{BodyProperties}
                    <td class=""text-center"">
                      <button type=""button"" class=""btn btn-xs btn-danger"" @click=""deleteSpa(item.id)""><i class=""fa fa-trash""></i></button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
            <div class=""box-footer"">
              <paginate v-if=""meta.totalPage !== undefined""
                        :page-count=""meta.totalPage""
                        :click-handler=""callPage""
                        :prev-text=""'Prev'""
                        :next-text=""'Next'""
                        :container-class=""'pagination'""
                        :page-class=""'page-item'"">
              </paginate>
            </div>
          </div>
        </div>
</template>

<script>
import Paginate from ""vuejs-paginate/src/components/Paginate.vue"";
import apibase from ""@/assets/js/api/baseApi.js"";
export default {
  layout: ""master"",
  data() {
    return {
      meta: {},
      query: {
        page: 1,
        rowPerPage: 50,
        keyword: """"
      },
      {InstanceName}s: []  
    };
  },
  components: {
    Paginate
  },
  methods: {
    
    Gets() {
      console.log(this.query);
      apibase
        .gets(this.$authToken, this.query, ""{InstanceName}s"")
        .then(rs => {
          this.{InstanceName}s = rs.data.data;
          this.meta = rs.data.meta;
        })
        .catch(err => {
          console.log(err);
        });
    },
    callPage(page) {
      this.query.page = page;
      this.getData();
    },
    load({InstanceName}) {
      this.$emit(""get{InstanceName}"", {InstanceName});
    },
    delete{InstanceName}(id) {
      apibase
        .delete(this.$authToken, id, ""{InstanceName}s"")
        .then(rs => {
          this.Gets();
        })
        .catch(err => {
          console.log(err);
        });
    }
  },
  beforeMount() {
    this.Gets();
  }
};
</script>

<style>
</style>

";

    }
}
