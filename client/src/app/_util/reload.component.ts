import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-reload',
    template: `
    <p>
      reload works!
    </p>
  `,
    styles: [
    ]
})
export class ReloadComponent implements OnInit {

    constructor(protected router: Router) { }

    ngOnInit(): void {
    }

    reloadComponent(self: boolean, urlToNavigateTo?: string) {
        //skipLocationChange:true means dont update the url to / when navigating
        const url = self ? this.router.url : urlToNavigateTo;
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate([`/${url}`])
        })
    }

    reloadPage() {
        window.location.reload()
    }

}