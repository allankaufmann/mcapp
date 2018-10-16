/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { McappWebTestModule } from '../../../test.module';
import { TextAntwortComponent } from 'app/entities/text-antwort/text-antwort.component';
import { TextAntwortService } from 'app/entities/text-antwort/text-antwort.service';
import { TextAntwort } from 'app/shared/model/text-antwort.model';

describe('Component Tests', () => {
    describe('TextAntwort Management Component', () => {
        let comp: TextAntwortComponent;
        let fixture: ComponentFixture<TextAntwortComponent>;
        let service: TextAntwortService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [TextAntwortComponent],
                providers: []
            })
                .overrideTemplate(TextAntwortComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(TextAntwortComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(TextAntwortService);
        });

        it('Should call load all on init', () => {
            // GIVEN
            const headers = new HttpHeaders().append('link', 'link;link');
            spyOn(service, 'query').and.returnValue(
                of(
                    new HttpResponse({
                        body: [new TextAntwort(123)],
                        headers
                    })
                )
            );

            // WHEN
            comp.ngOnInit();

            // THEN
            expect(service.query).toHaveBeenCalled();
            expect(comp.textAntworts[0]).toEqual(jasmine.objectContaining({ id: 123 }));
        });
    });
});
