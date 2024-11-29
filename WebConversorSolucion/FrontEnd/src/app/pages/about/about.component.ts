import { Component,OnInit } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})


export class AboutComponent implements OnInit {
  responsiveOptions!: any[];

  // Lista de productos
  products: Persona[] = [
    {
      id: '1000',
      code: 'f230fh0g3',
      image: '',
      name: 'Mision',
      description: 'Develop a reliable, efficient, and user-friendly tool for currency conversion, helping businesses and banks manage international transactions securely, accurately, and promptly. ' +
        'We promote financial transparency and the optimization of economic processes through innovative technological solutions.',
      category: 'Accessories',
      quantity: 24,
      inventoryStatus: 'INSTOCK',
      rating: 5,
    },
    {
      id: '1001',
      code: 'nvklal433',
      image: '',
      name: 'Vision',
      description: ' To be the leading global currency conversion platform, recognized for its accuracy, agility, and ability to integrate advanced features that facilitate international trade and financial management. We aim to build a technological bridge that eliminates economic barriers and drives the growth of our users.',
      category: 'Accessories',
      quantity: 61,
      inventoryStatus: 'OUTOFSTOCK',
      rating: 4,
    },
    {
      id: '1002',
      code: 'nvklal434',
      image: '',
      name: 'Values',
      description: 'Precision: We guarantee accurate and real-time updated data for informed financial decisions.' +
        'Innovation: We are committed to providing cutting-edge technology to simplify and automate economic processes.' +
        'Transparency: We prioritize clarity and honesty in all our operations and services.' +
        'Security: We ensure the protection of our users\' data and transactions.' +
        'Responsibility: We work with ethics and commitment to create a positive impact on the global financial ecosystem.' +
        'Collaboration: We foster strategic partnerships with businesses, banks, and users to provide solutions tailored to their needs.',
      category: 'Fitness',
      quantity: 50,
      inventoryStatus: 'INSTOCK',
      rating: 3,
    },

    {
      id: '1002',
      code: 'nvklal434',
      image: '',
      name: 'Values',
      description: 'Integrity, innovation, and collaboration drive everything we do, ensuring excellence in every step.',
      category: 'Fitness',
      quantity: 50,
      inventoryStatus: 'INSTOCK',
      rating: 3,
    },

    {
      id: '1002',
      code: 'nvklal434',
      image: '',
      name: 'Values',
      description: 'Integrity, innovation, and collaboration drive everything we do, ensuring excellence in every step.',
      category: 'Fitness',
      quantity: 50,
      inventoryStatus: 'INSTOCK',
      rating: 3,
    },

    {
      id: '1002',
      code: 'nvklal434',
      image: '',
      name: 'Values',
      description: 'Integrity, innovation, and collaboration drive everything we do, ensuring excellence in every step.',
      category: 'Fitness',
      quantity: 50,
      inventoryStatus: 'INSTOCK',
      rating: 3,
    },
  ];

  constructor() {}

  ngOnInit() {
    this.responsiveOptions = [
      {
        breakpoint: '1199px',
        numVisible: 3,
        numScroll: 3,
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 2,
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1,
      },
    ];
  }
}

interface Persona {
  id: string;
  code: string;
  name: string;
  image: string;
  description: string;
  category: string;
  quantity: number;
  inventoryStatus: string;
  rating: number;
};
