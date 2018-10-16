/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { BildAntwortDetailComponent } from 'app/entities/bild-antwort/bild-antwort-detail.component';
import { BildAntwort } from 'app/shared/model/bild-antwort.model';

describe('Component Tests', () => {
    describe('BildAntwort Management Detail Component', () => {
        let comp: BildAntwortDetailComponent;
        let fixture: ComponentFixture<BildAntwortDetailComponent>;
        const route = ({ data: of({ bildAntwort: new BildAntwort(123) }) } as any) as ActivatedRoute;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [BildAntwortDetailComponent],
                providers: [{ provide: ActivatedRoute, useValue: route }]
            })
                .overrideTemplate(BildAntwortDetailComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(BildAntwortDetailComponent);
            comp = fixture.componentInstance;
        });

        describe('OnInit', () => {
            it('Should call load all on init', () => {
                // GIVEN

                // WHEN
                comp.ngOnInit();

                // THEN
                expect(comp.bildAntwort).toEqual(jasmine.objectContaining({ id: 123 }));
            });
        });
    });
});
