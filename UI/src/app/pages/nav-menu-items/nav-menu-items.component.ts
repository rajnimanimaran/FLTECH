import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NavItem } from '../navbar/navbar.component';

@Component({
  selector: 'app-nav-menu-items',
  templateUrl: './nav-menu-items.component.html',
  styleUrls: ['./nav-menu-items.component.scss']
})
export class NavMenuItemsComponent implements OnInit {

  @Input() items: NavItem[];
  @ViewChild('childMenu') public childMenu;
  constructor() { }

  ngOnInit() {
  }

}
