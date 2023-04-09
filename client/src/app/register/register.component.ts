import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Register } from '../_models/register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {  
  @Output() cancelRegister = new EventEmitter();
  model: Register

  constructor (private accountService: AccountService) {
    this.model = {displayName:'',email:'',password:'',userName:''}
  }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => this.cancel()
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
