/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { TextAntwortDetailComponent } from 'app/entities/text-antwort/text-antwort-detail.component';
import { TextAntwort } from 'app/shared/model/text-antwort.model';

describe('Component Tests', () => {
    describe('TextAntwort Management Detail Component', () => {
        let comp: TextAntwortDetailComponent;
        let fixture: ComponentFixture<TextAntwortDetailComponent>;
        const route = ({ data: of({ textAntwort: new TextAntwort(123) }) } as any) as ActivatedRoute;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [TextAntwortDetailComponent],
                providers: [{ provide: ActivatedRoute, useValue: route }]
            })
                .overrideTemplate(TextAntwortDetailComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(TextAntwortDetailComponent);
            comp = fixture.componentInstance;
        });

        describe('OnInit', () => {
            it('Should call load all on init', () => {
                // GIVEN

                // WHEN
                comp.ngOnInit();

                // THEN
                expect(comp.textAntwort).toEqual(jasmine.objectContaining({ id: 123 }));
            });
        });
    });
});
