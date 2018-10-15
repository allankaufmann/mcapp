/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { AntwortDetailComponent } from 'app/entities/antwort/antwort-detail.component';
import { Antwort } from 'app/shared/model/antwort.model';

describe('Component Tests', () => {
    describe('Antwort Management Detail Component', () => {
        let comp: AntwortDetailComponent;
        let fixture: ComponentFixture<AntwortDetailComponent>;
        const route = ({ data: of({ antwort: new Antwort(123) }) } as any) as ActivatedRoute;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [AntwortDetailComponent],
                providers: [{ provide: ActivatedRoute, useValue: route }]
            })
                .overrideTemplate(AntwortDetailComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(AntwortDetailComponent);
            comp = fixture.componentInstance;
        });

        describe('OnInit', () => {
            it('Should call load all on init', () => {
                // GIVEN

                // WHEN
                comp.ngOnInit();

                // THEN
                expect(comp.antwort).toEqual(jasmine.objectContaining({ id: 123 }));
            });
        });
    });
});
