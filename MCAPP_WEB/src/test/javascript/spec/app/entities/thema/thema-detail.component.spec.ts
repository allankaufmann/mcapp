/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { ThemaDetailComponent } from 'app/entities/thema/thema-detail.component';
import { Thema } from 'app/shared/model/thema.model';

describe('Component Tests', () => {
    describe('Thema Management Detail Component', () => {
        let comp: ThemaDetailComponent;
        let fixture: ComponentFixture<ThemaDetailComponent>;
        const route = ({ data: of({ thema: new Thema(123) }) } as any) as ActivatedRoute;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [ThemaDetailComponent],
                providers: [{ provide: ActivatedRoute, useValue: route }]
            })
                .overrideTemplate(ThemaDetailComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(ThemaDetailComponent);
            comp = fixture.componentInstance;
        });

        describe('OnInit', () => {
            it('Should call load all on init', () => {
                // GIVEN

                // WHEN
                comp.ngOnInit();

                // THEN
                expect(comp.thema).toEqual(jasmine.objectContaining({ id: 123 }));
            });
        });
    });
});
