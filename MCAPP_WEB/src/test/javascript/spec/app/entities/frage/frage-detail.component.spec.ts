/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { FrageDetailComponent } from 'app/entities/frage/frage-detail.component';
import { Frage } from 'app/shared/model/frage.model';

describe('Component Tests', () => {
    describe('Frage Management Detail Component', () => {
        let comp: FrageDetailComponent;
        let fixture: ComponentFixture<FrageDetailComponent>;
        const route = ({ data: of({ frage: new Frage(123) }) } as any) as ActivatedRoute;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [FrageDetailComponent],
                providers: [{ provide: ActivatedRoute, useValue: route }]
            })
                .overrideTemplate(FrageDetailComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(FrageDetailComponent);
            comp = fixture.componentInstance;
        });

        describe('OnInit', () => {
            it('Should call load all on init', () => {
                // GIVEN

                // WHEN
                comp.ngOnInit();

                // THEN
                expect(comp.frage).toEqual(jasmine.objectContaining({ id: 123 }));
            });
        });
    });
});
