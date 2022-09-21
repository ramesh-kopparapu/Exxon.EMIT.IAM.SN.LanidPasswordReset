import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation:ViewEncapsulation.None
})
export class AppComponent implements OnInit{
  title = 'webapp';
  showPassword:boolean=false;
  inputType="password";
  password:string=null;
  email:string=null;
  token:string=null;

  message:string= null;
  errorMsg:string = "An error occurred while recording your response. Please try again later!";

  constructor(private http: HttpClient,
    private route: ActivatedRoute){

  }

  ngOnInit(){
    this.route.queryParams
      .subscribe(params => {
        console.log(params); 
        if(params.token == undefined){
           this.message = this.errorMsg;
         }
         else{
          this.message = "Submitting your response ...";
          this.token = params.token;
          this.loadUser();
         }
      }
    );
  }

  loadUser(){
     this.http.get("/.auth/me").subscribe((response:any)=>{
       if(response.clientPrincipal){
        this.email = response.clientPrincipal.userDetails;
        this.invokeAzureFunction();
       }
       else{
         this.message = "UnAuthorized";
       }

     })
  }

  invokeAzureFunction(){
    var url = environment.azureFunctionUrl;
    url = url.replace("[token]",this.token);
    this.http.get(url).subscribe((response:any)=>{
      if(response.password){
        this.password = response.password;
      }
      else{
        this.message = "No records to show"
      }
     });
  }

  togglePassword(){
    this.showPassword = !this.showPassword;
    if(this.showPassword){
      this.inputType="text";
    }else{
      this.inputType = "password";
    }
  }
}
